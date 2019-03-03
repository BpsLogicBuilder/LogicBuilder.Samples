using CheckMySymptoms.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Services.Store;

namespace CheckMySymptoms.WindowsStore
{
    public sealed class StoreAccessHelper
    {
        private StoreAccessHelper() { }

        const string adFreeSubscriptionStoreId = "adFreeCheckMySymptoms";

        #region Fields
        private static volatile StoreAccessHelper instance = null;
        private StoreContext storeContext = null;
        private StoreProduct subscriptionStoreProduct;
        #endregion Fields

        #region Properties
        public static StoreAccessHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StoreAccessHelper();
                }

                return instance;
            }
        }
        #endregion Properties

        #region Methods
        public async Task<bool> HasLicense()
        {
            if (storeContext == null)
            {
                storeContext = StoreContext.GetDefault();

                //IInitializeWithWindow initWindow = (IInitializeWithWindow)(object)storeContext;
                //initWindow.Initialize(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);
            }

            StoreAppLicense license = await storeContext.GetAppLicenseAsync();
            return license != null && license.IsActive;
        }

        public async Task<bool> CheckIfUserHasAdFreeSubscriptionAsync()
        {
            if (storeContext == null)
            {
                storeContext = StoreContext.GetDefault();

                //IInitializeWithWindow initWindow = (IInitializeWithWindow)(object)storeContext;
                //initWindow.Initialize(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);
            }

            StoreAppLicense appLicense = await storeContext.GetAppLicenseAsync();

            // Check if the customer has the rights to the subscription.
            foreach (var addOnLicense in appLicense.AddOnLicenses)
            {
                StoreLicense license = addOnLicense.Value;
                if (license.SkuStoreId.StartsWith(adFreeSubscriptionStoreId))
                {
                    if (license.IsActive)
                    {
                        // The expiration date is available in the license.ExpirationDate property.
                        return true;
                    }
                }
            }

            // The customer does not have a license to the subscription.
            return false;
        }

        private async Task<StoreProduct> GetAdFreeSubscriptionProductAsync()
        {
            // Load the sellable add-ons for this app and check if the trial is still 
            // available for this customer. If they previously acquired a trial they won't 
            // be able to get a trial again, and the StoreProduct.Skus property will 
            // only contain one SKU.
            StoreProductQueryResult result =
                await storeContext.GetAssociatedStoreProductsAsync(new string[] { "Durable" });

            if (result.ExtendedError != null)
            {
                await Helpers.ShowDialog(ResourceStringNames.errorGettingAddOnsFormat.GetResourceString().FormatString(result.ExtendedError));
                return null;
            }

            // Look for the product that represents the subscription.
            foreach (var item in result.Products)
            {
                StoreProduct product = item.Value;
                if (product.StoreId == adFreeSubscriptionStoreId)
                {
                    return product;
                }
            }

            await Helpers.ShowDialog(ResourceStringNames.subscriptionToPurchaseNotFound.GetResourceString());
            return null;
        }

        private async Task PromptUserToPurchaseAdFreeAsync()
        {
            // Request a purchase of the subscription product. If a trial is available it will be offered 
            // to the customer. Otherwise, the non-trial SKU will be offered.
            StorePurchaseResult result = await subscriptionStoreProduct.RequestPurchaseAsync();

            // Capture the error message for the operation, if any.
            string extendedError = string.Empty;
            if (result.ExtendedError != null)
            {
                extendedError = result.ExtendedError.Message;
            }

            switch (result.Status)
            {
                case StorePurchaseStatus.Succeeded:
                    await Helpers.ShowDialog(ResourceStringNames.adFreePurchaseSuccessful.GetResourceString());
                    break;

                case StorePurchaseStatus.NotPurchased:
                    await Helpers.ShowDialog(ResourceStringNames.addOnNotPurchasedFormat.GetResourceString().FormatString(extendedError));
                    break;

                case StorePurchaseStatus.ServerError:
                case StorePurchaseStatus.NetworkError:
                    System.Diagnostics.Debug.WriteLine(ResourceStringNames.addOnErrorDuringPurchaseFormat.GetResourceString().FormatString(extendedError));
                    break;

                case StorePurchaseStatus.AlreadyPurchased:
                    System.Diagnostics.Debug.WriteLine(ResourceStringNames.addOnAlreadyPurchasedFormat.GetResourceString().FormatString(extendedError));
                    break;
            }
        }

        public async Task SetupAdFreeSubscriptionInfoAsync()
        {
            if (storeContext == null)
            {
                storeContext = StoreContext.GetDefault();

                //IInitializeWithWindow initWindow = (IInitializeWithWindow)(object)storeContext;
                //initWindow.Initialize(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);
            }

            bool userOwnsSubscription = await CheckIfUserHasAdFreeSubscriptionAsync();
            if (userOwnsSubscription)
            {
                // Unlock all the subscription add-on features here.
                return;
            }

            // Get the StoreProduct that represents the subscription add-on.
            subscriptionStoreProduct = await GetAdFreeSubscriptionProductAsync();
            if (subscriptionStoreProduct == null)
            {
                return;
            }

            // Check if the first SKU is a trial and notify the customer that a trial is available.
            // If a trial is available, the Skus array will always have 2 purchasable SKUs and the
            // first one is the trial. Otherwise, this array will only have one SKU.
            StoreSku sku = subscriptionStoreProduct.Skus[0];
            if (sku.SubscriptionInfo.HasTrialPeriod)
            {
                // You can display the subscription trial info to the customer here. You can use 
                // sku.SubscriptionInfo.TrialPeriod and sku.SubscriptionInfo.TrialPeriodUnit 
                // to get the trial details.
            }
            else
            {
                // You can display the subscription purchase info to the customer here. You can use 
                // sku.SubscriptionInfo.BillingPeriod and sku.SubscriptionInfo.BillingPeriodUnit
                // to provide the renewal details.
            }

            // Prompt the customer to purchase the subscription.
            await PromptUserToPurchaseAdFreeAsync();
        }
        #endregion Methods
    }
}
