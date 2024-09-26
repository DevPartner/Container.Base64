import { HttpClient, HttpRequest, HttpResponse } from '@microsoft/signalr';

export class CustomHttpClient extends HttpClient {
  private readonly _innerClient: HttpClient;
  private readonly token: string;

  constructor(innerClient: HttpClient, token: string) {
    super();
    this.token = token; // Store the token
    this._innerClient = innerClient;
  }

  public send(request: HttpRequest): Promise<HttpResponse> {
    // Modify the request headers or other properties here
    if (request.headers) {
      request.headers["Authorization"] = this.token;
    }

    // Call the original send method with the modified request
    return this._innerClient.send(request);
  }
}
