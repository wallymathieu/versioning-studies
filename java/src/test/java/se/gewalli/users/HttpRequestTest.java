package se.gewalli.users;

import static org.assertj.core.api.Assertions.assertThat;
import static org.junit.Assert.assertEquals;
import static se.gewalli.users.RequestHelper.assertListOfV1Body;
import static se.gewalli.users.RequestHelper.assertListOfV2Body;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.context.SpringBootTest.WebEnvironment;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.boot.web.server.LocalServerPort;
import org.springframework.core.ParameterizedTypeReference;
import org.springframework.http.*;
import org.springframework.test.context.junit4.SpringRunner;
import se.gewalli.users.model.BaseUser;
import se.gewalli.users.model.V1User;
import se.gewalli.users.model.V2User;

import java.nio.file.LinkOption;
import java.util.List;
class RequestHelper{
    public static void assertListOfV2Body(ResponseEntity<List<BaseUser>> exchange) {
        List<BaseUser> body = exchange.getBody();
        assertEquals(2, body.size());
        assertThat( body.get(0)).isInstanceOf(V2User.class);
        assertEquals("Firstname",((V2User) body.get(0)).getName().getFirstName());
    }

    public static void assertListOfV1Body(ResponseEntity<List<BaseUser>> exchange) {
        List<BaseUser> body = exchange.getBody();
        assertEquals(2, body.size());
        assertThat( body.get(0)).isInstanceOf(V1User.class);
        assertEquals("Firstname Lastname",((V1User) body.get(0)).getName());
    }
    public static ResponseEntity<List<BaseUser>> queryAcceptApi(TestRestTemplate restTemplate, int port, String accept){
        HttpHeaders headers = new HttpHeaders();
        headers.set("Accept", accept);
        HttpEntity<Void> entity = new HttpEntity<>(headers);

        return restTemplate.exchange("http://localhost:" + port + "/users", HttpMethod.GET, entity, new ParameterizedTypeReference<List<BaseUser>>() {});
    }
    public static ResponseEntity<List<BaseUser>> queryParamApi(TestRestTemplate restTemplate, int port, String version){
        HttpHeaders headers = new HttpHeaders();
        headers.set("Accept", "application/json");
        HttpEntity<Void> entity = new HttpEntity<>(headers);
        return restTemplate.exchange("http://localhost:" + port + "/user"+(version==null?"":"?api-version="+version), HttpMethod.GET, entity, new ParameterizedTypeReference<List<BaseUser>>() {});
    }

}
@RunWith(SpringRunner.class)
@SpringBootTest(webEnvironment = WebEnvironment.RANDOM_PORT)
public class HttpRequestTest {

    @LocalServerPort
    private int port;

    @Autowired
    private TestRestTemplate restTemplate;
    private ResponseEntity<List<BaseUser>> queryAcceptApi(String accept){
        return RequestHelper.queryAcceptApi(restTemplate,port,accept);
    }
    private ResponseEntity<List<BaseUser>> queryParamApi(String version){
        return RequestHelper.queryParamApi(restTemplate,port,version);
    }
    @Test
    public void testSuccessfullyQueryVersionParameterV2() throws Exception {
        ResponseEntity<List<BaseUser>> exchange = queryParamApi("2.0");
        assertEquals(HttpStatus.OK, exchange.getStatusCode());
        assertEquals("application/json;charset=UTF-8", exchange.getHeaders().get("Content-Type").get(0));
        assertListOfV2Body(exchange);
    }
    @Test
    public void testSuccessfullyQueryVersionParameterV1() throws Exception {
        ResponseEntity<List<BaseUser>> exchange = queryParamApi("1.0");
        assertEquals(HttpStatus.OK, exchange.getStatusCode());
        assertEquals("application/json;charset=UTF-8", exchange.getHeaders().get("Content-Type").get(0));
        assertListOfV1Body(exchange);
    }

    @Test
    public void testSuccessfullyQueryVersionParameterDefaultV2() throws Exception {
        ResponseEntity<List<BaseUser>> exchange = queryParamApi(null);
        assertEquals(HttpStatus.OK, exchange.getStatusCode());
        assertEquals("application/json;charset=UTF-8", exchange.getHeaders().get("Content-Type").get(0));
        assertListOfV2Body(exchange);
    }

    @Test
    public void testSuccessfullyQueryAcceptDefaultV2() throws Exception {
        ResponseEntity<List<BaseUser>> exchange =queryAcceptApi("application/json");
        assertEquals(HttpStatus.OK, exchange.getStatusCode());
        assertEquals("application/json;charset=UTF-8", exchange.getHeaders().get("Content-Type").get(0));
        assertListOfV2Body(exchange);
    }
    @Test
    public void testSuccessfullyQueryAcceptV2() throws Exception {
        ResponseEntity<List<BaseUser>> exchange =queryAcceptApi("application/se.gewalli.users.v2+json");
        assertEquals(HttpStatus.OK, exchange.getStatusCode());
        assertEquals("application/se.gewalli.users.v2+json;charset=UTF-8", exchange.getHeaders().get("Content-Type").get(0));
        assertListOfV2Body(exchange);
    }
    @Test
    public void testSuccessfullyQueryAcceptV1() throws Exception {
        ResponseEntity<List<BaseUser>> exchange =queryAcceptApi("application/se.gewalli.users.v1+json");

        assertEquals(HttpStatus.OK, exchange.getStatusCode());
        assertEquals("application/se.gewalli.users.v1+json;charset=UTF-8", exchange.getHeaders().get("Content-Type").get(0));
        assertListOfV1Body(exchange);
    }

    @Test
    public void testNotAcceptableVersion() throws Exception {
        HttpHeaders headers = new HttpHeaders();
        headers.set("Accept", "application/se.gewalli.users.v21+json");
        HttpEntity<Void> entity = new HttpEntity<>(headers);

        ResponseEntity<String> exchange = restTemplate.exchange("http://localhost:" + port + "/users", HttpMethod.GET, entity, String.class);

        assertEquals(HttpStatus.NOT_ACCEPTABLE, exchange.getStatusCode());
    }
}
