<template>
  <v-app id="inspire">
    <v-content>
      <v-container fluid fill-height>
        <v-layout align-center justify-center>
          <v-flex xs12>
            <v-card class="elevation-12">
              <v-card-title>Lista de Pedidos</v-card-title>
              <v-data-table
                :items="formattedOrders"
                :headers="headers"
                :items-per-page="5"
              ></v-data-table>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
  </v-app>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, computed } from "vue";
import { ApiResponse } from "~/interfaces/apiResponse";

export default defineComponent({
  name: "OrderList",
  setup() {
    const orders = ref([]);
    const headers = ref([
      { text: "Produto", value: "product.name" },
      { text: "Quantidade", value: "quantity" },
      { text: "Valor Do produto", value: "product.valueFormatted" },
      { text: "Valor Total", value: "valorFormatted" },
      { text: "Data do pedido", value: "createdAt" },
    ]);

    const fetchOrders = async () => {
      try {
        const response = await fetch("http://localhost:5042/api/v1/order");

        const data: ApiResponse<any> = await response.json();

        if (data.success) {
          orders.value = data.result;
        } else {
          alert(`Erro: ${data.message}`);
        }
      } catch (error) {
        alert("Erro ao buscar pedidos. Por favor, tente novamente mais tarde.");
      }
    };

    const formatCurrency = (value: number) => {
      return value.toLocaleString("pt-BR", {
        style: "currency",
        currency: "BRL",
      });
    };

    const formatDate = (dateString: string) => {
      const date = new Date(dateString);
      const day = String(date.getDate()).padStart(2, "0");
      const month = String(date.getMonth() + 1).padStart(2, "0");
      const year = date.getFullYear();
      return `${day}-${month}-${year}`;
    };

    const formattedOrders = computed(() => {
      return orders.value.map((order: any) => ({
        ...order,
        product: {
          ...order.product,
          valueFormatted: formatCurrency(order.product.value),
        },
        valorFormatted: formatCurrency(order.valor),
        createdAt: formatDate(order.createdAt),
      }));
    });

    onMounted(fetchOrders);

    return {
      headers,
      formattedOrders,
    };
  },
});
</script>
