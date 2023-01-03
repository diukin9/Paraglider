import axios, { AxiosPromise } from "axios";

import { BasicAuthRequest } from "../typings/server";

export interface IAuthApi {
  basicAuth: (data: BasicAuthRequest) => AxiosPromise<void>;
  logout: () => AxiosPromise<void>;
}

export class AuthApi implements IAuthApi {
  public basicAuth(data: BasicAuthRequest) {
    const url = `basic-auth`;
    return axios.post(url, data);
  }

  public logout() {
    const url = `logout`;
    return axios.post(url);
  }
}
