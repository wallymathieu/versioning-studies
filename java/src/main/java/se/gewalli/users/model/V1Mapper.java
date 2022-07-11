package se.gewalli.users.model;

import se.gewalli.users.domain.User;
import se.gewalli.users.domain.UserRole;

import java.util.stream.Collectors;

public class V1Mapper {
    public static V1User map(User value)
    {
        return new V1User()
                .id(value.getId().getId())
                .email(value.getEmail())
                .isActive(value.isActive())
                .name(value.getName())
                .roles(value.getRoles().stream().map(V1Mapper::mapRole).collect(Collectors.toList()));
    }

    private static String mapRole(UserRole arg)
    {
        switch (arg)
        {
            case ADMINISTRATOR: return "A";
            case NORMAL: return "N";
            case SUPPORT: return "S";
            default: throw new RuntimeException(arg.toString());
        }
    }
}
