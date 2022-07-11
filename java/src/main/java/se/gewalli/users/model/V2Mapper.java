package se.gewalli.users.model;

import se.gewalli.users.domain.User;
import se.gewalli.users.domain.UserRole;

import java.util.stream.Collectors;

public class V2Mapper {
    public static V2User map(User value)
    {
        return new V2User().userUri("/users/" + (value.getId().getId()))
                .email(value.getEmail())
                .isActive(value.isActive())
                .name(new V2UserName().firstName(value.getFirstName()).lastName(value.getLastName()))
                .roles(value.getRoles().stream().map(V2Mapper::mapRole).collect(Collectors.toList()));
    }

    private static V2UserRole mapRole(UserRole arg)
    {
        switch (arg)
        {
            case Administrator: return V2UserRole.Administrator;
            case Support: return V2UserRole.Support;
            case Normal: return V2UserRole.Normal;
            default: throw new RuntimeException(arg.toString());
        }
    }
}
