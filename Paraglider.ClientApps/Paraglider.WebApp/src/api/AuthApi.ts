import { AxiosPromise } from "axios";

import { AuthProvider, BasicAuthRequest } from "../typings/server";
import { axiosClient } from "./Api";

export interface IAuthApi {
  signIn: (data: BasicAuthRequest) => AxiosPromise<void>;
  logout: () => AxiosPromise<void>;
  getExternalAuthUrl: (authProvider: AuthProvider) => string;
}

export class AuthApi implements IAuthApi {
  private readonly baseUrl = "api/v1/auth/web";

  public signIn(data: BasicAuthRequest) {
    const url = `${this.baseUrl}/sign-in`;
    return axiosClient.post(url, data);
  }

  public logout() {
    const url = `${this.baseUrl}/logout`;
    return axiosClient.post(url);
  }

  public getExternalAuthUrl(authProvider: AuthProvider): string {
    const baseUrl = axiosClient.defaults.baseURL;
    const url = `${this.baseUrl}/${authProvider}`;

    const urlObj = new URL(url, baseUrl);
    urlObj.searchParams.set("callback", window.location.href);

    return urlObj.toString();
  }
}
