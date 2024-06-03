<template>
  <TableList :itemType="itemType" :title="title" />
</template>

<script lang="ts">
export default {
  name: "ListOrderPage",
  data() {
    return {
      itemType: "order",
      title: "Listagem dos pedidos",
    };
  },
};
</script>
