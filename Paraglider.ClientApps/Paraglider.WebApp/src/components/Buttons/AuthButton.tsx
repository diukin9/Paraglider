import { useContext } from "react";

import { AuthModalContext } from "../../contexts/AuthModalContext";
import { UserContext } from "../../contexts/UserContext";
import { Button, ButtonProps } from "./Button/Button";

export const AuthButton = (props: ButtonProps) => {
  const { user } = useContext(UserContext);
  const authModal = useContext(AuthModalContext);

  const handleClick = () => {
    if (user) {
      props.onClick();
    } else {
      authModal.open();
    }
  };

  return <Button {...props} onClick={handleClick} />;
};
