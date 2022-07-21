package se.gewalli.users.domain;

import java.util.Collection;

import lombok.Value;
@Value
public class User {
    private UserId id;
    private String login;
    private String password;
    private String email;
    private boolean isActive;
    private String firstName;
    private String lastName;
    private String name;
    private Collection<UserRole> roles;
}
