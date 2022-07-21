package se.gewalli.users.model;

import org.springframework.validation.annotation.Validated;

import com.fasterxml.jackson.annotation.JsonProperty;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.Data;
import lombok.experimental.Accessors;

/**
 * V2UserName
 */
@Validated
@Accessors(chain = true,fluent = true)
@Data
public class V2UserName   {
  @JsonProperty("firstName")
  private String firstName = null;

  @JsonProperty("lastName")
  private String lastName = null;


  /**
   * Get firstName
   * @return firstName
   **/
  @Schema(description = "")
  
    public String getFirstName() {
    return firstName;
  }

  /**
   * Get lastName
   * @return lastName
   **/
  @Schema(description = "")
  
    public String getLastName() {
    return lastName;
  }


}
