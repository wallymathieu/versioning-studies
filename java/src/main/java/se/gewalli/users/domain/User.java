package se.gewalli.users.domain;

import java.util.Collection;

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

    public User(
            UserId Id,
            String Login,
            String Password,
            String Email,
            boolean IsActive,
            String FirstName,
            String LastName,
            String Name,
            Collection<UserRole> Roles){
        id = Id;
        login = Login;
        password = Password;
        email = Email;
        isActive = IsActive;
        firstName = FirstName;
        lastName = LastName;
        name = Name;
        roles = Roles;
    }

    public UserId getId() {
        return id;
    }

    public String getLogin() {
        return login;
    }

    public String getPassword() {
        return password;
    }

    public String getEmail() {
        return email;
    }

    public boolean isActive() {
        return isActive;
    }

    public String getFirstName() {
        return firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public String getName() {
        return name;
    }

    public Collection<UserRole> getRoles() {
        return roles;
    }
}
