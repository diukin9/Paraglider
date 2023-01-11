import { PropsWithChildren, useCallback, useState } from "react";

import { AuthCardModal } from "../../components/Modal/AuthCardModal";
import { AuthModalContext } from "./AuthModalContext";

export const AuthModalContextStore = ({ children }: PropsWithChildren) => {
  const [isOpen, setIsOpen] = useState(false);

  const close = useCallback(() => setIsOpen(false), []);

  const open = useCallback(() => setIsOpen(true), []);

  return (
    <AuthModalContext.Provider value={{ isOpen, close, open }}>
      {children}
      {isOpen && <AuthCardModal onClose={close} />}
    </AuthModalContext.Provider>
  );
};
