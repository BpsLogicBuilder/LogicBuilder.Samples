using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CheckMySymptoms.Forms.View
{
    public interface IVariableMetadataRepository
    {
        ICollection<VariableMetadata> GetMetadata();
    }

    public interface IPatientDataRepository
    {
        Task<Dictionary<string, VariableInfo>> GetData(Dictionary<string, VariableInfo> variables);
        Task SaveData(Dictionary<string, VariableInfo> variables);
        Task DeleteData();
    }

    //Need a ViewModel Project in Universal Windows class library which maps the the view classes.  Need to do this because of time constraints.

    public interface ILookUpsRepository
    {
        Task<ICollection<LookUpsViewModel>> GetItemsAsync(Expression<Func<LookUpsViewModel, bool>> filter = null, Expression<Func<IQueryable<LookUpsViewModel>, IQueryable<LookUpsViewModel>>> queryFunc = null);

        Task<ICollection<LookUpsViewModel>> GetByListId(string listId);
    }

    public class LookUpsRepository : ILookUpsRepository
    {
        public async Task<ICollection<LookUpsViewModel>> GetItemsAsync(Expression<Func<LookUpsViewModel, bool>> filter = null, Expression<Func<IQueryable<LookUpsViewModel>, IQueryable<LookUpsViewModel>>> queryFunc = null)
        {
            IQueryable<LookUpsViewModel> query = Cache.AsQueryable();
            if (filter != null)
                query = query.Where(filter);

            return queryFunc != null ? await Task.Run(() => queryFunc.Compile()(query).ToList()) : query.ToList();
        }

        public async Task<ICollection<LookUpsViewModel>> GetByListId(string listId)
        {
            if (listId == "sex")
                return await GetItemsAsync(l => l.ListName == listId, q => q.OrderBy(l => l.Value));

            if (listId == "age")
                return await GetItemsAsync(l => l.ListName == listId, q => q.OrderBy(l => l.NumericValue));

            throw new ArgumentException("{1C7B150B-F1A3-4A30-8A13-B3598D7E8C3D}");
        }

        private static readonly LookUpsViewModel[] Cache = new LookUpsViewModel[]
            {
                new  LookUpsViewModel { ListName = "sex", Text="Male", CharValue='M' },
                new  LookUpsViewModel { ListName = "sex", Text="Female", CharValue='F' },

                new  LookUpsViewModel { ListName = "age", Text="Zero",      IntegerValue=0 },
                new  LookUpsViewModel { ListName = "age", Text="One",       IntegerValue=1 },
                new  LookUpsViewModel { ListName = "age", Text="Two",       IntegerValue=2 },
                new  LookUpsViewModel { ListName = "age", Text="Three",     IntegerValue=3 },
                new  LookUpsViewModel { ListName = "age", Text="Four",      IntegerValue=4 },
                new  LookUpsViewModel { ListName = "age", Text="Five",      IntegerValue=5 },
                new  LookUpsViewModel { ListName = "age", Text="Six",       IntegerValue=6 },
                new  LookUpsViewModel { ListName = "age", Text="Seven",     IntegerValue=7 },
                new  LookUpsViewModel { ListName = "age", Text="Eight",     IntegerValue=8 },
                new  LookUpsViewModel { ListName = "age", Text="Nine",      IntegerValue=9 },

                new  LookUpsViewModel { ListName = "age", Text="Ten",       IntegerValue=10 },
                new  LookUpsViewModel { ListName = "age", Text="Eleven",    IntegerValue=11 },
                new  LookUpsViewModel { ListName = "age", Text="Twelve",    IntegerValue=12 },
                new  LookUpsViewModel { ListName = "age", Text="Thirteen",  IntegerValue=13 },
                new  LookUpsViewModel { ListName = "age", Text="Fourteen",  IntegerValue=14 },
                new  LookUpsViewModel { ListName = "age", Text="Fifteen",   IntegerValue=15 },
                new  LookUpsViewModel { ListName = "age", Text="Sixteen",   IntegerValue=16 },
                new  LookUpsViewModel { ListName = "age", Text="Seventeen", IntegerValue=17 },
                new  LookUpsViewModel { ListName = "age", Text="Eighteen",  IntegerValue=18 },
                new  LookUpsViewModel { ListName = "age", Text="Nineteen",  IntegerValue=19 },

                new  LookUpsViewModel { ListName = "age", Text="Twenty",           IntegerValue=20 },
                new  LookUpsViewModel { ListName = "age", Text="Twenty One",       IntegerValue=21 },
                new  LookUpsViewModel { ListName = "age", Text="Twenty Two",       IntegerValue=22 },
                new  LookUpsViewModel { ListName = "age", Text="Twenty Three",     IntegerValue=23 },
                new  LookUpsViewModel { ListName = "age", Text="Twenty Four",      IntegerValue=24 },
                new  LookUpsViewModel { ListName = "age", Text="Twenty Five",      IntegerValue=25 },
                new  LookUpsViewModel { ListName = "age", Text="Twenty Six",       IntegerValue=26 },
                new  LookUpsViewModel { ListName = "age", Text="Twenty Seven",     IntegerValue=27 },
                new  LookUpsViewModel { ListName = "age", Text="Twenty Eight",     IntegerValue=28 },
                new  LookUpsViewModel { ListName = "age", Text="Twenty Nine",      IntegerValue=29 },

                new  LookUpsViewModel { ListName = "age", Text="Thirty",           IntegerValue=30 },
                new  LookUpsViewModel { ListName = "age", Text="Thirty One",       IntegerValue=31 },
                new  LookUpsViewModel { ListName = "age", Text="Thirty Two",       IntegerValue=32 },
                new  LookUpsViewModel { ListName = "age", Text="Thirty Three",     IntegerValue=33 },
                new  LookUpsViewModel { ListName = "age", Text="Thirty Four",      IntegerValue=34 },
                new  LookUpsViewModel { ListName = "age", Text="Thirty Five",      IntegerValue=35 },
                new  LookUpsViewModel { ListName = "age", Text="Thirty Six",       IntegerValue=36 },
                new  LookUpsViewModel { ListName = "age", Text="Thirty Seven",     IntegerValue=37 },
                new  LookUpsViewModel { ListName = "age", Text="Thirty Eight",     IntegerValue=38 },
                new  LookUpsViewModel { ListName = "age", Text="Thirty Nine",      IntegerValue=39 },

                new  LookUpsViewModel { ListName = "age", Text="Fourty",           IntegerValue=40 },
                new  LookUpsViewModel { ListName = "age", Text="Fourty One",       IntegerValue=41 },
                new  LookUpsViewModel { ListName = "age", Text="Fourty Two",       IntegerValue=42 },
                new  LookUpsViewModel { ListName = "age", Text="Fourty Three",     IntegerValue=43 },
                new  LookUpsViewModel { ListName = "age", Text="Fourty Four",      IntegerValue=44 },
                new  LookUpsViewModel { ListName = "age", Text="Fourty Five",      IntegerValue=45 },
                new  LookUpsViewModel { ListName = "age", Text="Fourty Six",       IntegerValue=46 },
                new  LookUpsViewModel { ListName = "age", Text="Fourty Seven",     IntegerValue=47 },
                new  LookUpsViewModel { ListName = "age", Text="Fourty Eight",     IntegerValue=48 },
                new  LookUpsViewModel { ListName = "age", Text="Fourty Nine",      IntegerValue=49 },

                new  LookUpsViewModel { ListName = "age", Text="Fifty",           IntegerValue=50 },
                new  LookUpsViewModel { ListName = "age", Text="Fifty One",       IntegerValue=51 },
                new  LookUpsViewModel { ListName = "age", Text="Fifty Two",       IntegerValue=52 },
                new  LookUpsViewModel { ListName = "age", Text="Fifty Three",     IntegerValue=53 },
                new  LookUpsViewModel { ListName = "age", Text="Fifty Four",      IntegerValue=54 },
                new  LookUpsViewModel { ListName = "age", Text="Fifty Five",      IntegerValue=55 },
                new  LookUpsViewModel { ListName = "age", Text="Fifty Six",       IntegerValue=56 },
                new  LookUpsViewModel { ListName = "age", Text="Fifty Seven",     IntegerValue=57 },
                new  LookUpsViewModel { ListName = "age", Text="Fifty Eight",     IntegerValue=58 },
                new  LookUpsViewModel { ListName = "age", Text="Fifty Nine",      IntegerValue=59 },

                new  LookUpsViewModel { ListName = "age", Text="Sixty",           IntegerValue=60 },
                new  LookUpsViewModel { ListName = "age", Text="Sixty One",       IntegerValue=61 },
                new  LookUpsViewModel { ListName = "age", Text="Sixty Two",       IntegerValue=62 },
                new  LookUpsViewModel { ListName = "age", Text="Sixty Three",     IntegerValue=63 },
                new  LookUpsViewModel { ListName = "age", Text="Sixty Four",      IntegerValue=64 },
                new  LookUpsViewModel { ListName = "age", Text="Sixty Five",      IntegerValue=65 },
                new  LookUpsViewModel { ListName = "age", Text="Sixty Six",       IntegerValue=66 },
                new  LookUpsViewModel { ListName = "age", Text="Sixty Seven",     IntegerValue=67 },
                new  LookUpsViewModel { ListName = "age", Text="Sixty Eight",     IntegerValue=68 },
                new  LookUpsViewModel { ListName = "age", Text="Sixty Nine",      IntegerValue=69 },

                new  LookUpsViewModel { ListName = "age", Text="Seventy",           IntegerValue=70 },
                new  LookUpsViewModel { ListName = "age", Text="Seventy One",       IntegerValue=71 },
                new  LookUpsViewModel { ListName = "age", Text="Seventy Two",       IntegerValue=72 },
                new  LookUpsViewModel { ListName = "age", Text="Seventy Three",     IntegerValue=73 },
                new  LookUpsViewModel { ListName = "age", Text="Seventy Four",      IntegerValue=74 },
                new  LookUpsViewModel { ListName = "age", Text="Seventy Five",      IntegerValue=75 },
                new  LookUpsViewModel { ListName = "age", Text="Seventy Six",       IntegerValue=76 },
                new  LookUpsViewModel { ListName = "age", Text="Seventy Seven",     IntegerValue=77 },
                new  LookUpsViewModel { ListName = "age", Text="Seventy Eight",     IntegerValue=78 },
                new  LookUpsViewModel { ListName = "age", Text="Seventy Nine",      IntegerValue=79 },

                new  LookUpsViewModel { ListName = "age", Text="Eighty",           IntegerValue=80 },
                new  LookUpsViewModel { ListName = "age", Text="Eighty One",       IntegerValue=81 },
                new  LookUpsViewModel { ListName = "age", Text="Eighty Two",       IntegerValue=82 },
                new  LookUpsViewModel { ListName = "age", Text="Eighty Three",     IntegerValue=83 },
                new  LookUpsViewModel { ListName = "age", Text="Eighty Four",      IntegerValue=84 },
                new  LookUpsViewModel { ListName = "age", Text="Eighty Five",      IntegerValue=85 },
                new  LookUpsViewModel { ListName = "age", Text="Eighty Six",       IntegerValue=86 },
                new  LookUpsViewModel { ListName = "age", Text="Eighty Seven",     IntegerValue=87 },
                new  LookUpsViewModel { ListName = "age", Text="Eighty Eight",     IntegerValue=88 },
                new  LookUpsViewModel { ListName = "age", Text="Eighty Nine",      IntegerValue=89 },

                new  LookUpsViewModel { ListName = "age", Text="Ninety",           IntegerValue=90 },
                new  LookUpsViewModel { ListName = "age", Text="Ninety One",       IntegerValue=91 },
                new  LookUpsViewModel { ListName = "age", Text="Ninety Two",       IntegerValue=92 },
                new  LookUpsViewModel { ListName = "age", Text="Ninety Three",     IntegerValue=93 },
                new  LookUpsViewModel { ListName = "age", Text="Ninety Four",      IntegerValue=94 },
                new  LookUpsViewModel { ListName = "age", Text="Ninety Five",      IntegerValue=95 },
                new  LookUpsViewModel { ListName = "age", Text="Ninety Six",       IntegerValue=96 },
                new  LookUpsViewModel { ListName = "age", Text="Ninety Seven",     IntegerValue=97 },
                new  LookUpsViewModel { ListName = "age", Text="Ninety Eight",     IntegerValue=98 },
                new  LookUpsViewModel { ListName = "age", Text="Ninety Nine",      IntegerValue=99 },

                new  LookUpsViewModel { ListName = "age", Text="A Hundred",           IntegerValue=100 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And One",       IntegerValue=101 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Two",       IntegerValue=102 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Three",     IntegerValue=103 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Four",      IntegerValue=104 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Five",      IntegerValue=105 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Six",       IntegerValue=106 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Seven",     IntegerValue=107 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Eight",     IntegerValue=108 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Nine",      IntegerValue=109 },

                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Ten",    IntegerValue=110 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Eleven",    IntegerValue=111 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Twelve",    IntegerValue=112 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Thirteen",  IntegerValue=113 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Fourteen",  IntegerValue=114 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Fifteen",   IntegerValue=115 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Sixteen",   IntegerValue=116 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Seventeen", IntegerValue=117 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Eighteen",  IntegerValue=118 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Nineteen",  IntegerValue=119 },

                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Twenty",           IntegerValue=120 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Twenty One",       IntegerValue=121 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Twenty Two",       IntegerValue=122 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Twenty Three",     IntegerValue=123 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Twenty Four",      IntegerValue=124 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Twenty Five",      IntegerValue=125 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Twenty Six",       IntegerValue=126 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Twenty Seven",     IntegerValue=127 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Twenty Eight",     IntegerValue=128 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Twenty Nine",      IntegerValue=129 },

                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Thirty",           IntegerValue=130 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Thirty One",       IntegerValue=131 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Thirty Two",       IntegerValue=132 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Thirty Three",     IntegerValue=133 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Thirty Four",      IntegerValue=134 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Thirty Five",      IntegerValue=135 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Thirty Six",       IntegerValue=136 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Thirty Seven",     IntegerValue=137 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Thirty Eight",     IntegerValue=138 },
                new  LookUpsViewModel { ListName = "age", Text="A Hundred And Thirty Nine",      IntegerValue=139 },
            };
    }

    public class LookUpsViewModel
    {
        public int LookUpsID { get; set; }

        public string Text { get; set; }

        public string ListName { get; set; }

        public string Value { get; set; }

        public int? IntegerValue { get; set; }

        public double? NumericValue { get; set; }

        public bool? BooleanValue { get; set; }

        public System.DateTime? DateTimeValue { get; set; }

        public char? CharValue { get; set; }

        public System.Guid? GuidValue { get; set; }

        public System.TimeSpan? TimeSpanValue { get; set; }

        public int Order { get; set; }
    }

    public class VariableMetadata
    {
        public VariableMetadata()
        {
        }

        public VariableMetadata(string variableName, string variableType, string metadata, string table)
        {
            VariableName = variableName;
            VariableType = variableType;
            Metadata = metadata;
            Table = table;
        }

        public string VariableName { get; set; }
        public string VariableType { get; set; }
        public string Metadata { get; set; }
        public string Table { get; set; }
    }
}
