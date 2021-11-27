import {
  Box,
  Button,
  Checkbox,
  Modal,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  TextFieldProps,
  Typography,
} from "@mui/material";
import useStore from "@Stores";
import React, { useEffect, useState } from "react";
import NumberFormat from "react-number-format";

import { checkbox, modal, sendButton, tableBodyCell, tableHeaderCell, tableWrapper, TextInput, title } from "./style";

interface Props {
  isOpen: boolean;
  handleClose: () => void;
  sendCallback: (groups: Array<string>, openPeriod?: number) => void;
}

function SurveySendingModal(props: Props) {
  const { isOpen, handleClose, sendCallback } = props;
  const { groupStore } = useStore();
  const { groupsNumbers } = groupStore;

  useEffect(() => {
    groupStore.getGroupsNumbers();
  }, [groupStore]);

  const close = () => {
    setSelected([]);
    handleClose();
  };

  const [selected, setSelected] = useState<Array<string>>([]);

  const handleAllSelected = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.checked) {
      setSelected(groupsNumbers);
    } else {
      setSelected([]);
    }
  };

  const handleRowSelected = (event: React.ChangeEvent<HTMLInputElement>, group: string) => {
    if (event.target.checked) {
      setSelected(selected.concat(group));
    } else {
      setSelected(selected.filter((g) => g !== group));
    }
  };

  const checkSelection = (group: string) => selected.indexOf(group) !== -1;

  const [time, setTime] = useState<string>();

  const handleSend = () => {
    if (selected.length) {
      const period = time
        ?.split(":")
        .map((s) => parseInt(s.trim()))
        .reduceRight((prev, curr, i) => (prev || 0) + (curr * 60 ** (2 - i) || 0));
      sendCallback(selected, period);
      setSelected([]);
      handleClose();
    }
  };

  return (
    <Modal open={isOpen} onClose={close}>
      <Box sx={modal}>
        <Typography sx={title}>Select groups</Typography>
        <Box sx={tableWrapper}>
          <Table stickyHeader padding="checkbox">
            <TableHead>
              <TableRow>
                <TableCell sx={tableHeaderCell}>
                  <Checkbox sx={checkbox} onChange={handleAllSelected} />
                </TableCell>
                <TableCell sx={tableHeaderCell}>
                  <Typography align="center">Number</Typography>
                </TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {groupsNumbers.map((group) => (
                <TableRow selected={checkSelection(group)} key={group}>
                  <TableCell sx={tableBodyCell}>
                    <Checkbox
                      sx={checkbox}
                      checked={checkSelection(group)}
                      onChange={(e) => handleRowSelected(e, group)}
                    />
                  </TableCell>
                  <TableCell align="center" sx={tableBodyCell}>
                    {group}
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </Box>
        <NumberFormat
          customInput={PeriodInput}
          format="##:##:##"
          placeholder="hh:mm:ss"
          onChange={(e) => setTime(e.target.value)}
        />
        <Button sx={sendButton} onClick={handleSend} variant="outlined">
          Send
        </Button>
      </Box>
    </Modal>
  );
}

export default SurveySendingModal;

const PeriodInput: React.FC<TextFieldProps> = (props: TextFieldProps) => (
  <TextInput {...props} label="OpenPeriod" />
);
