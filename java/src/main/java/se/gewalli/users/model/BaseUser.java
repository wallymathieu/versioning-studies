package se.gewalli.users.model;

import com.fasterxml.jackson.annotation.JsonSubTypes;
import com.fasterxml.jackson.annotation.JsonTypeInfo;

@JsonTypeInfo(use = JsonTypeInfo.Id.NAME, include = JsonTypeInfo.As.PROPERTY)
@JsonSubTypes({
        @JsonSubTypes.Type(value = V1User.class, name = "V1User"),

        @JsonSubTypes.Type(value = V2User.class, name = "V2User") }
)
public interface BaseUser {
}
