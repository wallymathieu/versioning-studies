package se.gewalli.users.store;

import se.gewalli.users.domain.User;

import java.util.Collection;

public interface UserRepository {
    Collection<User> getUsers();

}
