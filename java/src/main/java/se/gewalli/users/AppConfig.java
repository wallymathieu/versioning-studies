package se.gewalli.users;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import se.gewalli.users.domain.User;
import se.gewalli.users.domain.UserId;
import se.gewalli.users.domain.UserRole;
import se.gewalli.users.store.UserRepository;

import java.util.Arrays;
import java.util.Collection;

@Configuration
public class AppConfig {

    @Bean
    public UserRepository userRepository() {
        return new UserRepository() {
            @Override
            public Collection<User> getUsers() {
                return Arrays.asList(
                        new User(new UserId(1),"myUser","Password","email@email.se",true,"Firstname","Lastname","Firstname Lastname",
                            Arrays.asList(UserRole.Normal)),
                        new User(new UserId(2),"support","Password2","email2@email.se",true,"Firstname","Lastname","Firstname Lastname",
                            Arrays.asList(UserRole.Support)));
            }
        };
    }
}
