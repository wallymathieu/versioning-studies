package se.gewalli.users.api;

import java.util.List;
import java.util.stream.Collectors;

import javax.servlet.http.HttpServletRequest;
import javax.validation.Valid;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.fasterxml.jackson.databind.ObjectMapper;

import se.gewalli.users.model.*;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.enums.ParameterIn;
import io.swagger.v3.oas.annotations.media.Schema;
import se.gewalli.users.store.UserRepository;

@RestController
public class UserApiController implements UserApi {

    private static final Logger log = LoggerFactory.getLogger(UserApiController.class);

    private final ObjectMapper objectMapper;

    private final HttpServletRequest request;
    private UserRepository userRepository;

    @org.springframework.beans.factory.annotation.Autowired
    public UserApiController(ObjectMapper objectMapper, HttpServletRequest request, UserRepository userRepository) {
        this.objectMapper = objectMapper;
        this.request = request;
        this.userRepository = userRepository;
    }

    public ResponseEntity<List<BaseUser>> userGet(String apiVersion) {
        if (apiVersion==null) apiVersion="";
        switch (apiVersion){
            case "1.0":
                return new ResponseEntity<List<BaseUser>>(
                        userRepository.getUsers().stream().map(V1Mapper::map).collect(Collectors.toList()), HttpStatus.OK);
            case "":
            case "2.0":
                return new ResponseEntity<List<BaseUser>>(
                        userRepository.getUsers().stream().map(V2Mapper::map).collect(Collectors.toList()), HttpStatus.OK);
            default:
                return new ResponseEntity<List<BaseUser>>(HttpStatus.BAD_REQUEST);
        }

    }

    @Override
    public ResponseEntity<List<BaseUser>> usersGet(String accept) {
        switch (accept){
            case "application/se.gewalli.users.v1+json":
                return new ResponseEntity<List<BaseUser>>(
                        userRepository.getUsers().stream().map(V1Mapper::map).collect(Collectors.toList()), HttpStatus.OK);
            case "application/json":
            case "application/se.gewalli.users.v2+json":
                return new ResponseEntity<List<BaseUser>>(
                        userRepository.getUsers().stream().map(V2Mapper::map).collect(Collectors.toList()), HttpStatus.OK);

            default:
                return new ResponseEntity<List<BaseUser>>(HttpStatus.BAD_REQUEST);
        }
    }

    public ResponseEntity<List<V1User>> v1UserGet() {
        return new ResponseEntity<List<V1User>>(
                userRepository.getUsers().stream().map(V1Mapper::map).collect(Collectors.toList()), HttpStatus.OK);
    }

    public ResponseEntity<List<V2User>> v2UserGet() {
        return new ResponseEntity<List<V2User>>(
                userRepository.getUsers().stream().map(V2Mapper::map).collect(Collectors.toList()), HttpStatus.OK);
    }
}
