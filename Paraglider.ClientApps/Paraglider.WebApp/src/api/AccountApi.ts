import { AxiosPromise } from "axios";

import { RegisterUserRequest } from "../typings/server";
import { axiosClient } from "./Api";

export interface IAccountAPi {
  register: (data: RegisterUserRequest) => AxiosPromise<void>;
}

export class AccountApi implements IAccountAPi {
  private readonly baseUrl = "api/v1/account";

  public register(data: RegisterUserRequest) {
    const url = `${this.baseUrl}/register`;
    return axiosClient.post(url, data);
  }
}
