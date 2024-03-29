/**
 * NOTE: This class is auto generated by the swagger code generator program (3.0.34).
 * https://github.com/swagger-api/swagger-codegen
 * Do not edit the class manually.
 */
package se.gewalli.users.api;

import java.util.List;

import javax.validation.Valid;

import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;

import se.gewalli.users.model.BaseUser;
import se.gewalli.users.model.V1User;
import se.gewalli.users.model.V2User;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.enums.ParameterIn;
import io.swagger.v3.oas.annotations.media.ArraySchema;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;

@Validated
public interface UserApi {

    @Operation(summary = "", description = "Get users with request param for API version", tags={ "User" })
    @ApiResponses(value = { 
        @ApiResponse(responseCode = "200", description = "Success", 
                     content = @Content(
                                    mediaType = "application/json", 
                                    array = @ArraySchema(schema = @Schema(oneOf = {V1User.class, V2User.class})))) })
    @RequestMapping(value = "/user",
        produces = { "application/json", "text/json" }, 
        method = RequestMethod.GET)
    ResponseEntity<List<BaseUser>> userGet(
        @Parameter(in = ParameterIn.QUERY, description = "The requested API version" ,schema=@Schema(allowableValues = {"2.0","1.0",""}))
        @Valid
        @RequestParam(value = "api-version", required = false, defaultValue="2.0") 
        String apiVersion);

    @Operation(summary = "", description = "Get users with media type param for API version", tags={ "User" })
    @ApiResponses(value = {
            @ApiResponse(responseCode = "200", description = "Success",
                    content = @Content(
                            mediaType = "application/json",
                            array = @ArraySchema(schema = @Schema(oneOf = {V1User.class, V2User.class})))) })
    @RequestMapping(value = "/users",
            produces = {"application/json", "application/se.gewalli.users.v1+json", "application/se.gewalli.users.v2+json"}, // NOTE: order does matter for Accept: */* and application/*
            method = RequestMethod.GET)
    ResponseEntity<List<BaseUser>> usersGet(
            @Parameter(in = ParameterIn.HEADER, description = "The requested API version" ,schema=@Schema(allowableValues = {"application/json", "application/se.gewalli.users.v1+json", "application/se.gewalli.users.v2+json"}))
            @Valid
            @RequestHeader(value = "accept", required = true, defaultValue="application/json")
            String accept);

    @Operation(summary = "", description = "Get users", tags={ "User" })
    @ApiResponses(value = { 
        @ApiResponse(responseCode = "200", description = "Success", 
            content = @Content(mediaType = "application/json", 
                               array = @ArraySchema(schema = @Schema(implementation = V1User.class)))) })
    @RequestMapping(value="/v1/user",
        method = RequestMethod.GET,
        produces = {"application/json"})
    ResponseEntity<List<V1User>> v1UserGet();


    @Operation(summary = "", description = "Get users", tags={ "User" })
    @ApiResponses(value = { 
        @ApiResponse(responseCode = "200", description = "Success", 
            content = @Content(mediaType = "application/json", 
                               array = @ArraySchema(schema = @Schema(implementation = V2User.class)))) })
    @RequestMapping(value="/v2/user",
        method = RequestMethod.GET,
        produces = {"application/json"})
    ResponseEntity<List<V2User>> v2UserGet();
}

