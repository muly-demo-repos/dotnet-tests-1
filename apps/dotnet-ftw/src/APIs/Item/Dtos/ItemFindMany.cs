using DotnetFtw.APIs.Common;
using DotnetFtw.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFtw.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class ItemFindMany : FindManyInput<Item, ItemWhereInput> { }
