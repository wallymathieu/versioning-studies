package io.swagger.model;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

import javax.validation.Valid;

import org.springframework.validation.annotation.Validated;

import com.fasterxml.jackson.annotation.JsonProperty;

import io.swagger.v3.oas.annotations.media.Schema;

/**
 * V2User
 */
@Validated
public class V2User   {
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

  public V2User userUri(String userUri) {
    this.userUri = userUri;
    return this;
  }

  /**
   * Get userUri
   * @return userUri
   **/
  @Schema(description = "")
  
    public String getUserUri() {
    return userUri;
  }

  public void setUserUri(String userUri) {
    this.userUri = userUri;
  }

  public V2User email(String email) {
    this.email = email;
    return this;
  }

  /**
   * Get email
   * @return email
   **/
  @Schema(description = "")
  
    public String getEmail() {
    return email;
  }

  public void setEmail(String email) {
    this.email = email;
  }

  public V2User isActive(Boolean isActive) {
    this.isActive = isActive;
    return this;
  }

  /**
   * Get isActive
   * @return isActive
   **/
  @Schema(description = "")
  
    public Boolean isIsActive() {
    return isActive;
  }

  public void setIsActive(Boolean isActive) {
    this.isActive = isActive;
  }

  public V2User name(V2UserName name) {
    this.name = name;
    return this;
  }

  /**
   * Get name
   * @return name
   **/
  @Schema(description = "")
  
    @Valid
    public V2UserName getName() {
    return name;
  }

  public void setName(V2UserName name) {
    this.name = name;
  }

  public V2User roles(List<V2UserRole> roles) {
    this.roles = roles;
    return this;
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
  @Schema(description = "")
      @Valid
    public List<V2UserRole> getRoles() {
    return roles;
  }

  public void setRoles(List<V2UserRole> roles) {
    this.roles = roles;
  }


  @Override
  public boolean equals(java.lang.Object o) {
    if (this == o) {
      return true;
    }
    if (o == null || getClass() != o.getClass()) {
      return false;
    }
    V2User v2User = (V2User) o;
    return Objects.equals(this.userUri, v2User.userUri) &&
        Objects.equals(this.email, v2User.email) &&
        Objects.equals(this.isActive, v2User.isActive) &&
        Objects.equals(this.name, v2User.name) &&
        Objects.equals(this.roles, v2User.roles);
  }

  @Override
  public int hashCode() {
    return Objects.hash(userUri, email, isActive, name, roles);
  }

  @Override
  public String toString() {
    StringBuilder sb = new StringBuilder();
    sb.append("class V2User {\n");
    
    sb.append("    userUri: ").append(toIndentedString(userUri)).append("\n");
    sb.append("    email: ").append(toIndentedString(email)).append("\n");
    sb.append("    isActive: ").append(toIndentedString(isActive)).append("\n");
    sb.append("    name: ").append(toIndentedString(name)).append("\n");
    sb.append("    roles: ").append(toIndentedString(roles)).append("\n");
    sb.append("}");
    return sb.toString();
  }

  /**
   * Convert the given object to string with each line indented by 4 spaces
   * (except the first line).
   */
  private String toIndentedString(java.lang.Object o) {
    if (o == null) {
      return "null";
    }
    return o.toString().replace("\n", "\n    ");
  }
}
