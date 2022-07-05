/**
 * NOTE: This class is auto generated by the swagger code generator program (3.0.34).
 * https://github.com/swagger-api/swagger-codegen
 * Do not edit the class manually.
 */
package io.swagger.api;

import java.util.List;

import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import io.swagger.model.V1User;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.ArraySchema;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;

@Validated
public interface V1Api {

    @Operation(summary = "", description = "Get users", tags={ "User" })
    @ApiResponses(value = { 
        @ApiResponse(responseCode = "200", description = "Success", 
            content = @Content(mediaType = "application/vnd.example.v1+json", 
                               array = @ArraySchema(schema = @Schema(implementation = V1User.class)))) })
    @RequestMapping(value="/User",
        method = RequestMethod.GET,
        produces = {"application/vnd.example.v1+json"},
        consumes = {"application/vnd.example.v1+json"})
    ResponseEntity<List<V1User>> v1UserGet();

}
