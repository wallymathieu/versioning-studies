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
            case ADMINISTRATOR: return V2UserRole.ADM;
            case SUPPORT: return V2UserRole.SUP;
            case NORMAL: return V2UserRole.USR;
            default: throw new RuntimeException(arg.toString());
        }
    }
}
