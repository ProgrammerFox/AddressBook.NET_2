using AddressBook_dotnet6.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook_Dotnet6.View
{
    public static class AddressBookCliExtensions
    {
        public static string AsTable<T>(this AddressBook<T> addressBook)
            where T : BasePerson
        {
            if (!addressBook.Any()) return "";

            string bookAsString = addressBook[0].ToJson()
                .Replace("{", "")
                .Replace("}", "")
                .Replace("\"", "")
                .Replace(" ", "");

            List<string> headers = bookAsString.Split(',')
                .Select(x => x.Split(':')[0])
                .ToList();

            List<List<string>> bookTable = addressBook.Select(x => x.ToJson()
                                            .Replace("{", "")
                                            .Replace("}", "")
                                            .Replace("\"", "")
                                            .Split(',')
                                            .Select(x => x.Split(':')[1])
                                                    .ToList())
                                            .ToList();


            headers.Insert(0, "id");

            bookTable = bookTable.Select((x, i) =>
            {
                x.Insert(0, $"{i + 1}");
                return x;
            }).ToList();

            var spaces = headers.Select(x => x.Length).ToArray();

            foreach (var bookRow in bookTable)
            {
                for (int i = 0; i < bookRow.Count; i++)
                {
                    spaces[i] = Math.Max(spaces[i], bookRow[i].Length);
                }
            }

            var headerString = new StringBuilder($"+-{"-".Repeat(spaces.Sum() + (spaces.Length - 1) * 3)}-+\n");

            var newHeaders = headers.Zip(spaces, (r, s) => r + " ".Repeat(s - r.Length)).ToArray();
            headerString.AppendLine($"| {string.Join(" | ", newHeaders)} |");

            headerString.AppendLine($"|-{"-".Repeat(spaces.Sum() + (spaces.Length - 1) * 3)}-|");

            foreach (var bookRow in bookTable)
            {
                var newBookRow = bookRow.Zip(spaces, (r, s) => r + " ".Repeat(s - r.Length)).ToArray();
                headerString.AppendLine($"| {string.Join(" | ", newBookRow)} |");
            }

            headerString.AppendLine($"+-{"-".Repeat(spaces.Sum() + (spaces.Length - 1) * 3)}-+");

            return headerString.ToString();
        }
    }
}
