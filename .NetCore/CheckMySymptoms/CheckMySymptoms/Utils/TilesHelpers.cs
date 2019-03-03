using NotificationsExtensions.TileContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace CheckMySymptoms.Utils
{
    internal static class TilesHelpers
    {
        private static TileNotification CreateNotifiction(string largeImageName, string wideImageName, string mediumImageName, string smallImageName, string notificationTag, string captionText)
        {
            return GetTileNotification
            (
                GetLargeTile
                (
                    TileContentFactory.CreateTileSquare310x310ImageAndText01()
                ).CreateNotification()
            );

            TileNotification GetTileNotification(TileNotification tileNotification)
            {
                tileNotification.Tag = notificationTag;
                return tileNotification;
            }

            ITileSquare310x310ImageAndText01 GetLargeTile(ITileSquare310x310ImageAndText01 largeTile)
            {
                largeTile.TextCaptionWrap.Text = ResourceStringNames.notificationText01.GetResourceString();
                largeTile.Image.Src = $"ms-appx:///Tiles/{largeImageName}.png";
                largeTile.Wide310x150Content = GetWideTile(TileContentFactory.CreateTileWide310x150ImageAndText01());

                return largeTile;
            }


            ITileWide310x150ImageAndText01 GetWideTile(ITileWide310x150ImageAndText01 largeTile)
            {
                largeTile.TextCaptionWrap.Text = captionText;
                largeTile.Image.Src = $"ms-appx:///Tiles/{wideImageName}.png";
                largeTile.Square150x150Content = GetMediumTile(TileContentFactory.CreateTileSquare150x150Image());

                return largeTile;
            }

            ITileSquare150x150Image GetMediumTile(ITileSquare150x150Image mediumTile)
            {
                mediumTile.Image.Src = $"ms-appx:///Tiles/{mediumImageName}.png";
                mediumTile.Square71x71Content = GetSmallTile(TileContentFactory.CreateTileSquare71x71Image());
                return mediumTile;
            }

            ITileSquare71x71Image GetSmallTile(ITileSquare71x71Image smallTile)
            {
                smallTile.Image.Src = $"ms-appx:///Tiles/{smallImageName}.png";
                return smallTile;
            }
        }

        internal static void CreateTiles()
        {

            //First Notification
            TileUpdateManager.CreateTileUpdaterForApplication().Update
            (
                CreateNotifiction
                (
                    "Large01",
                    "Wide01",
                    "Medium01",
                    "Small01",
                    "Tag01",
                    ResourceStringNames.notificationText01.GetResourceString()
                )
            );

            //Second Notification
            TileUpdateManager.CreateTileUpdaterForApplication().Update
            (
                CreateNotifiction
                (
                    "Large02",
                    "Wide02",
                    "Medium02",
                    "Small02",
                    "Tag02",
                    ResourceStringNames.notificationText02.GetResourceString()
                )
            );

            //Third Notification
            TileUpdateManager.CreateTileUpdaterForApplication().Update
            (
                CreateNotifiction
                (
                    "Large03",
                    "Wide03",
                    "Medium03",
                    "Small03",
                    "Tag03",
                    ResourceStringNames.notificationText03.GetResourceString()
                )
            );

            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
        }
    }
}
