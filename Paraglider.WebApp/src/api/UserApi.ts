import axios, { AxiosPromise } from "axios";

import { User } from "../typings/server";

export interface IUserApi {
  getUser: () => AxiosPromise<User>;
}

export class UserApi implements IUserApi {
  private readonly baseUrl = "api/v1/user";

  public getUser() {
    const url = `${this.baseUrl}`;
    console.log(axios.defaults);
    return axios.get<User>(url);
  }
}
