using System.ComponentModel.DataAnnotations;
using DotnetFtw.Core.Enums;

namespace DotnetFtw.APIs;

public class OrderCustomDto
{
    [Required()]
    public string Prop1 { get; set; }

    [Required()]
    public int Prop2 { get; set; }

    [Required()]
    public OrderCustomEnumEnum CustomOption { get; set; }
}
