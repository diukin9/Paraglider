import { Button } from "../../Buttons";
import {
  DefaultContentWrapper,
  HeaderRoot,
  HeaderWrapper,
} from "./Header.styles";
import Logo from "/images/paraglider-logo.svg";

export const Header = () => {
  return (
    <HeaderRoot>
      <HeaderWrapper>
        <DefaultContentWrapper>
          <img src={Logo} alt="Логотип ПараПлан" />
          <Button>Войти</Button>
        </DefaultContentWrapper>
      </HeaderWrapper>
    </HeaderRoot>
  );
};
