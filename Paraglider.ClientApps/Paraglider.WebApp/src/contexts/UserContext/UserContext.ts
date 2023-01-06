import React from "react";

import { User } from "../../typings/server";

export interface IUserState {
  user?: User;
  setUser: (user?: User) => void;
}

const defaultState: IUserState = {
  setUser: () => {
    throw new Error("Default context state: function setUser");
  },
};

export const UserContext = React.createContext<IUserState>(defaultState);
