package se.gewalli.users.configuration;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import springfox.documentation.service.Contact;
import springfox.documentation.spi.DocumentationType;
import springfox.documentation.spring.web.plugins.Docket;
import springfox.documentation.builders.RequestHandlerSelectors;
import springfox.documentation.builders.ApiInfoBuilder;
import springfox.documentation.service.ApiInfo;
import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.info.Info;
import io.swagger.v3.oas.models.info.License;

@javax.annotation.Generated(value = "io.swagger.codegen.v3.generators.java.SpringCodegen", date = "2022-07-05T19:29:26.052Z[GMT]")
@Configuration
public class SwaggerDocumentationConfig {

    @Bean
    public Docket customImplementation(){
        return new Docket(DocumentationType.OAS_30)
                .select()
                    .apis(RequestHandlerSelectors.basePackage("se.gewalli.users.api"))
                    .build()
                .directModelSubstitute(org.threeten.bp.LocalDate.class, java.sql.Date.class)
                .directModelSubstitute(org.threeten.bp.OffsetDateTime.class, java.util.Date.class)
                .apiInfo(apiInfo());
    }

    ApiInfo apiInfo() {
        return new ApiInfoBuilder()
            .title("Sample API")
            .description("A sample application with Swagger, Swashbuckle, and API versioning.")
            .license("MIT")
            .licenseUrl("https://opensource.org/licenses/MIT")
            .termsOfServiceUrl("")
            .version("2.0")
            .contact(new Contact("","", "bill.mei@somewhere.com"))
            .build();
    }

    @Bean
    public OpenAPI openApi() {
        return new OpenAPI()
            .info(new Info()
                .title("Sample API")
                .description("A sample application with Swagger, Swashbuckle, and API versioning.")
                .termsOfService("")
                .version("2.0")
                .license(new License()
                    .name("MIT")
                    .url("https://opensource.org/licenses/MIT"))
                .contact(new io.swagger.v3.oas.models.info.Contact()
                    .email("bill.mei@somewhere.com")));
    }

}
