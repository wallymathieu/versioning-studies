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
 * V1User
 */
@Validated
@Accessors(chain = true,fluent = true)
@Data
public class V1User implements BaseUser {
  @JsonProperty("id")
  private Integer id = null;

  @JsonProperty("email")
  private String email = null;

  @JsonProperty("isActive")
  private Boolean isActive = null;

  @JsonProperty("name")
  private String name = null;

  @JsonProperty("roles")
  @Valid
  private List<String> roles = null;

  /**
   * Get id
   * @return id
   **/
  @Schema(description = "") public Integer getId() {
    return id;
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
  @Schema(description = "")
  
    public Boolean isIsActive() {
    return isActive;
  }

  /**
   * Get name
   * @return name
   **/
  @Schema(description = "")
  
    public String getName() {
    return name;
  }

  public V1User addRolesItem(String rolesItem) {
    if (this.roles == null) {
      this.roles = new ArrayList<String>();
    }
    this.roles.add(rolesItem);
    return this;
  }

  /**
   * Get roles
   * @return roles
   **/
  @Schema(description = "") public List<String> getRoles() {
    return roles;
  }
}
