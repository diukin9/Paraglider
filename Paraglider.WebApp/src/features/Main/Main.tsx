import { Button } from "../../components/Buttons";
import { PageTitle } from "../../components/Text";
import {
  BgImage,
  ButtonContainer,
  ListTitle,
  ListWrapper,
  MainContainer,
  OrderedList,
  OrderedListItem,
} from "./Main.styles";
import Newlyweds from "./images/newlyweds.png";

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
        <Button variant="default">Перейти к конфигуратору</Button>
        <Button variant="outlined">Подробнее</Button>
      </ButtonContainer>
    </MainContainer>
  );
};
