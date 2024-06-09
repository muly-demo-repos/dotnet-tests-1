using System.ComponentModel.DataAnnotations;
using DotnetFtw.Core.Enums;

namespace DotnetFtw.APIs;

public class CustomModuleCustomDtoOne
{
    [Required()]
    public string P1 { get; set; }

    [Required()]
    public string P2 { get; set; }

    [Required()]
    public CustomModuleCustomEnumOneEnum MemberEnum { get; set; }

    [Required()]
    public OrderAnotherCustomEnumEnum RemoteEnum { get; set; }
}
