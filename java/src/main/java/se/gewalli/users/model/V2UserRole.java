package se.gewalli.users.model;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonValue;
import io.swagger.annotations.ApiModel;

/**
 * Gets or Sets V2UserRole
 */
@ApiModel
public enum V2UserRole {
  USR,
  SUP,
  ADM;


  V2UserRole() {
  }

  @Override
  @JsonValue
  public String toString() {
    return name();
  }

  @JsonCreator
  public static V2UserRole fromValue(String text) {
    for (V2UserRole b : V2UserRole.values()) {
      if (b.name().equals(text)) {
        return b;
      }
    }
    return null;
  }
}
