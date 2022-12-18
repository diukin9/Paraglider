import styled from "styled-components";

export const CardRoot = styled.div`
  position: relative;
  padding: 40px 36px;
  width: 430px;
  height: 700px;
  border-radius: 12px;
  background: #ffffff;
  box-shadow: 0px 4px 30px rgba(136, 135, 135, 0.15);
  overflow: hidden;
`;

export const CardTitle = styled.h3`
  margin-bottom: 16px;
  font-weight: 700;
  font-size: 32px;
  line-height: 40px;
`;

export const TopRightImage = styled.img`
  position: absolute;
  top: 0;
  right: 0;
`;

export const CardBackButtonWrapper = styled.div`
  margin-bottom: 20px;
`;
