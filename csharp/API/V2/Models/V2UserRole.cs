using System.ComponentModel;

namespace API.V2;

public enum V2UserRole
{
    [Description("USR")]
    Normal,
    [Description("SUP")]
    Support,
    [Description("ADM")]
    Administrator
}