import styled from "styled-components";

export const ModalContainer = styled.div`
  z-index: 3000;
  position: fixed;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;

  background-color: rgba(0, 0, 0, 0.6);
  cursor: pointer;
`;

export const ModalScrollContainer = styled.div`
  padding: 20px;

  width: 100vw;
  height: 100vh;

  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;

  overflow-y: auto;
`;

export const ModalContent = styled.div`
  cursor: default;
`;
