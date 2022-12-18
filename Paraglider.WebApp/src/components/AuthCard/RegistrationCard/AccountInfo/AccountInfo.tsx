import { Button } from "../../../Buttons";
import { BackButton } from "../../../Buttons/BackButton";
import { ControlsGroup, Input, Label } from "../../../FormControls";
import { CardBackButtonWrapper, CardRoot, CardTitle, TopRightImage } from "../../AuthCard.styles";
import { GirlIcon } from "../GirlIcon";
import { ButtonWrapper } from "./AccountInfo.styles";
import Circles from "./images/circles.svg";

interface AccountInfoProps {
  onGoBack: () => void;
}

export const AccountInfo = ({ onGoBack }: AccountInfoProps) => {
  return (
    <CardRoot>
      <TopRightImage src={Circles} />

      <CardBackButtonWrapper>
        <BackButton onClick={onGoBack} />
      </CardBackButtonWrapper>

      <CardTitle>Регистрация</CardTitle>

      <GirlIcon />

      <ControlsGroup marginBottom={24}>
        <Label>Ваше имя</Label>
        <Input type="text" />
      </ControlsGroup>

      <ButtonWrapper>
        <Button variant="default" onClick={() => null}>
          Завершить
        </Button>
      </ButtonWrapper>
    </CardRoot>
  );
};
