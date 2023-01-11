import React from "react";

import { AccountApi, AuthApi, CitiesApi, IApi, UserApi } from "../../api";

const defaultValue: IApi = {
  accountApi: new AccountApi(),
  authApi: new AuthApi(),
  citiesApi: new CitiesApi(),
  userApi: new UserApi(),
};

export const ApiContext = React.createContext<IApi>(defaultValue);
