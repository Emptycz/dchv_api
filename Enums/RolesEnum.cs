using dchv_api.Attributes;

namespace dchv_api.Enums;

public enum RolesEnum {
  [StringValue("admin")]
  Admin = 1,

  [StringValue("user")]
  User = 2,
}