import Logo from "/images/paraglider-logo.svg";

import { useOpening } from "../../../hooks/useOpening";
import { Button } from "../../Buttons";
import { AuthCardModal } from "../../Modal/AuthCardModal";
import { DefaultContentWrapper, HeaderRoot, HeaderWrapper } from "./Header.styles";

export const Header = () => {
  const authCardModal = useOpening();

  return (
    <>
      <HeaderRoot>
        <HeaderWrapper>
          <DefaultContentWrapper>
            <img src={Logo} alt="Логотип ПараПлан" />
            <Button onClick={authCardModal.open}>Войти</Button>
          </DefaultContentWrapper>
        </HeaderWrapper>
      </HeaderRoot>

      {authCardModal.isOpen && <AuthCardModal onClose={authCardModal.close} />}
    </>
  );
};
