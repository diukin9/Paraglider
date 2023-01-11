import React from "react";

export interface IAuthState {
  isOpen: boolean;
  close: () => void;
  open: () => void;
}

const defaultState: IAuthState = {
  isOpen: false,
  close: () => {
    throw new Error("Default AuthModalContext state: function close");
  },
  open: () => {
    throw new Error("Default AuthModalContext state: function open");
  },
};

export const AuthModalContext = React.createContext<IAuthState>(defaultState);
