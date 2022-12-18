import Logo from "/images/paraglider-logo.svg";

import { Button } from "../../Buttons";
import { DefaultContentWrapper, HeaderRoot, HeaderWrapper } from "./Header.styles";

export const Header = () => {
  const handleLogIn = () => {
    return null;
  };

  return (
    <HeaderRoot>
      <HeaderWrapper>
        <DefaultContentWrapper>
          <img src={Logo} alt="Логотип ПараПлан" />
          <Button onClick={handleLogIn}>Войти</Button>
        </DefaultContentWrapper>
      </HeaderWrapper>
    </HeaderRoot>
  );
};
