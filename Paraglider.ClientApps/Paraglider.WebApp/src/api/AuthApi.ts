import axios, { AxiosPromise } from "axios";

import { BasicAuthRequest } from "../typings/server";

export interface IAuthApi {
  signIn: (data: BasicAuthRequest) => AxiosPromise<void>;
  logout: () => AxiosPromise<void>;
}

export class AuthApi implements IAuthApi {
  private readonly baseUrl = "api/v1/auth/web";

  public signIn(data: BasicAuthRequest) {
    const url = `${this.baseUrl}/sign-in`;
    return axios.post(url, data);
  }

  public logout() {
    const url = `${this.baseUrl}/logout`;
    return axios.post(url);
  }
}
