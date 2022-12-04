import styled from "styled-components";

export const BgImage = styled.img`
  position: absolute;
  top: 0;
  right: 110px;
`;

export const ListTitle = styled.h3`
  margin-bottom: 20px;
  font-size: 20px;
`;

export const OrderedList = styled.ol`
  counter-reset: item;
  list-style-type: none;
`;

export const OrderedListItem = styled.li`
  counter-increment: item;
  font-size: 20px;

  :not(:last-child) {
    margin-bottom: 16px;
  }

  ::before {
    content: counter(item);

    display: inline-block;
    width: 12px;
    margin-right: 12px;

    font-weight: 900;
    font-size: 24px;
    color: ${({ theme }) => theme.textColors.primary};
  }
`;

export const ListWrapper = styled.div`
  margin-bottom: 40px;
`;

export const ButtonContainer = styled.div`
  display: flex;
  gap: 30px;
`;
