import React from "react";

import { User } from "../../typings/server";

export interface IUserState {
  user?: User;
  fetchUser: () => Promise<void>;
  setUser: (user: User | undefined) => void;
}

const defaultState: IUserState = {
  fetchUser: () => {
    throw new Error("Default UserContext state: function fetchUser");
  },
  setUser: () => {
    throw new Error("Default UserContext state: function setUser");
  },
};

export const UserContext = React.createContext<IUserState>(defaultState);
