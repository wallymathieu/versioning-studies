package io.swagger.api;

import java.io.IOException;
import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.validation.Valid;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.fasterxml.jackson.databind.ObjectMapper;

import io.swagger.model.V2User;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.enums.ParameterIn;
import io.swagger.v3.oas.annotations.media.Schema;

@RestController
public class UserApiController implements UserApi {

    private static final Logger log = LoggerFactory.getLogger(UserApiController.class);

    private final ObjectMapper objectMapper;

    private final HttpServletRequest request;

    @org.springframework.beans.factory.annotation.Autowired
    public UserApiController(ObjectMapper objectMapper, HttpServletRequest request) {
        this.objectMapper = objectMapper;
        this.request = request;
    }

    public ResponseEntity<List<V2User>> userGet(@Parameter(in = ParameterIn.QUERY, description = "The requested API version" ,schema=@Schema( defaultValue="2.0")) @Valid @RequestParam(value = "api-version", required = false, defaultValue="2.0") String apiVersion) {
        String accept = request.getHeader("Accept");
        if (accept != null && accept.contains("application/json")) {
            try {
                return new ResponseEntity<List<V2User>>(objectMapper.readValue("[ {\n  \"roles\" : [ 0, 0 ],\n  \"name\" : {\n    \"firstName\" : \"firstName\",\n    \"lastName\" : \"lastName\"\n  },\n  \"userUri\" : \"http://example.com/aeiou\",\n  \"isActive\" : true,\n  \"email\" : \"email\"\n}, {\n  \"roles\" : [ 0, 0 ],\n  \"name\" : {\n    \"firstName\" : \"firstName\",\n    \"lastName\" : \"lastName\"\n  },\n  \"userUri\" : \"http://example.com/aeiou\",\n  \"isActive\" : true,\n  \"email\" : \"email\"\n} ]", List.class), HttpStatus.NOT_IMPLEMENTED);
            } catch (IOException e) {
                log.error("Couldn't serialize response for content type application/json", e);
                return new ResponseEntity<List<V2User>>(HttpStatus.INTERNAL_SERVER_ERROR);
            }
        }

        return new ResponseEntity<List<V2User>>(HttpStatus.NOT_IMPLEMENTED);
    }

}
