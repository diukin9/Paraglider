import { useContext } from "react";

import Logo from "/images/paraglider-logo.svg";

import { ApiContext } from "../../../contexts";
import { AuthModalContext } from "../../../contexts/AuthModalContext";
import { UserContext } from "../../../contexts/UserContext";
import { Button } from "../../Buttons";
import { DefaultContentWrapper, HeaderRoot, HeaderWrapper } from "./Header.styles";

export const Header = () => {
  const { authApi } = useContext(ApiContext);
  const { user, setUser } = useContext(UserContext);
  const authModal = useContext(AuthModalContext);

  const handleLogout = async () => {
    try {
      await authApi.logout();
      setUser(undefined);
    } catch (e) {
      console.error(e);
    }
  };

  return (
    <>
      <HeaderRoot>
        <HeaderWrapper>
          <DefaultContentWrapper>
            <img src={Logo} alt="Логотип ПараПлан" />

            <div>
              {user ? (
                <Button variant="outlined" onClick={handleLogout}>
                  Выйти
                </Button>
              ) : (
                <Button variant="default" onClick={authModal.open}>
                  Войти
                </Button>
              )}
            </div>
          </DefaultContentWrapper>
        </HeaderWrapper>
      </HeaderRoot>
    </>
  );
};
