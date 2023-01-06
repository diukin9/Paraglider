import React from "react";

import { AccountApi, AuthApi, IApi, UserApi } from "../../api";

const defaultValue: IApi = {
  accountApi: new AccountApi(),
  authApi: new AuthApi(),
  userApi: new UserApi(),
};

export const ApiContext = React.createContext<IApi>(defaultValue);
