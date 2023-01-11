import { Button } from "../../components/Buttons";
import { AuthButton } from "../../components/Buttons/AuthButton";
import { PageTitle } from "../../components/Text";
import Newlyweds from "./images/newlyweds.png";
import {
  BgImage,
  ButtonContainer,
  ListTitle,
  ListWrapper,
  MainContainer,
  OrderedList,
  OrderedListItem,
} from "./Main.styles";

export const Main = () => {
  return (
    <MainContainer>
      <BgImage src={Newlyweds} alt="" />

      <PageTitle marginBottom={24}>
        Организация вашей свадьбы
        <br />
        <b>Легче, чем&nbsp;кажется</b>
      </PageTitle>

      <ListWrapper>
        <ListTitle>Всего три&nbsp;шага:</ListTitle>
        <OrderedList>
          <OrderedListItem>Выбрать категории</OrderedListItem>
          <OrderedListItem>Найти подходящий вариант</OrderedListItem>
          <OrderedListItem>Договориться с&nbsp;исполнителем</OrderedListItem>
        </OrderedList>
      </ListWrapper>

      <ButtonContainer>
        <AuthButton variant="default" onClick={() => null}>
          Перейти к конфигуратору
        </AuthButton>
        <Button variant="outlined" onClick={() => null}>
          Подробнее
        </Button>
      </ButtonContainer>
    </MainContainer>
  );
};
