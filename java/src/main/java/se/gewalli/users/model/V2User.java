package se.gewalli.users.model;

import java.util.ArrayList;
import java.util.List;
import javax.validation.Valid;

import org.springframework.validation.annotation.Validated;

import com.fasterxml.jackson.annotation.JsonProperty;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.Data;
import lombok.experimental.Accessors;

/**
 * V2User
 */
@Validated
@Accessors(chain = true,fluent = true)
@Data
public class V2User implements BaseUser {
  @JsonProperty("userUri")
  private String userUri = null;

  @JsonProperty("email")
  private String email = null;

  @JsonProperty("isActive")
  private Boolean isActive = null;

  @JsonProperty("name")
  private V2UserName name = null;

  @JsonProperty("roles")
  @Valid
  private List<V2UserRole> roles = null;

  /**
   * Get userUri
   * @return userUri
   **/
  @Schema(description = "")
  
    public String getUserUri() {
    return userUri;
  }

  /**
   * Get email
   * @return email
   **/
  @Schema(description = "") public String getEmail() {
    return email;
  }

  /**
   * Get isActive
   * @return isActive
   **/
  @Schema(description = "")  public Boolean isIsActive() {
    return isActive;
  }

  /**
   * Get name
   * @return name
   **/
  @Schema(description = "") @Valid public V2UserName getName() {
    return name;
  }

  public V2User addRolesItem(V2UserRole rolesItem) {
    if (this.roles == null) {
      this.roles = new ArrayList<V2UserRole>();
    }
    this.roles.add(rolesItem);
    return this;
  }

  /**
   * Get roles
   * @return roles
   **/
  @Schema(description = "") @Valid public List<V2UserRole> getRoles() {
    return roles;
  }

}
