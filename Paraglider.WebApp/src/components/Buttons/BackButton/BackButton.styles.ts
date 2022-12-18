import styled from "styled-components";

import { defaultButtonStyles } from "../Buttons.helpers";

export const BackButtonRoot = styled.button`
  ${defaultButtonStyles}

  font-size: 20px;
  line-height: 25px;
`;

export const BackButtonIcon = styled.img`
  margin-right: 12px;
  height: 15px;
  width: 7px;
`;
