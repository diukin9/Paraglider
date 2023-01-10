import axios from "axios";

import { AccountApi } from "./AccountApi";
import { AuthApi } from "./AuthApi";
import { UserApi } from "./UserApi";

export const axiosClient = axios.create({
  baseURL: import.meta.env.VITE_API_PROXY_URL,
  withCredentials: true,
});

export interface IApi {
  accountApi: AccountApi;
  authApi: AuthApi;
  userApi: UserApi;
}
