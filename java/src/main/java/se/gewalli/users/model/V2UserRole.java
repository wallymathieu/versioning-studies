package se.gewalli.users.model;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonValue;

/**
 * Gets or Sets V2UserRole
 */
public enum V2UserRole {
  Normal(0),
  Support(1),
  Administrator(2);

  private Integer value;

  V2UserRole(Integer value) {
    this.value = value;
  }

  @Override
  @JsonValue
  public String toString() {
    return String.valueOf(value);
  }

  @JsonCreator
  public static V2UserRole fromValue(String text) {
    for (V2UserRole b : V2UserRole.values()) {
      if (String.valueOf(b.value).equals(text)) {
        return b;
      }
    }
    return null;
  }
}
