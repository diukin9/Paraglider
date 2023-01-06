import { AccountApi } from "./AccountApi";
import { AuthApi } from "./AuthApi";
import { UserApi } from "./UserApi";

export interface IApi {
  accountApi: AccountApi;
  authApi: AuthApi;
  userApi: UserApi;
}
