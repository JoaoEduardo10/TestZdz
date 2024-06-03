<template>
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
            >
              <template v-slot:item.edit="{ item }">
                <v-btn class="blue" icon>
                  <NuxtLink :to="`/order/${item.id}`" class="white--text">
                    <v-icon>mdi-pencil</v-icon>
                  </NuxtLink>
                </v-btn>
              </template>
              <template v-slot:item.delete="{ item }">
                <v-btn class="red" icon @click="deleteOrder(item.id)">
                  <v-icon>mdi-delete</v-icon>
                </v-btn>
              </template>
            </v-data-table>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </v-content>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, computed } from "vue";
import { ApiResponse } from "~/interfaces/apiResponse";

export default defineComponent({
  name: "OrderList",
  data() {
    return {
      orders: [],
      headers: [
        { text: "Produto", value: "product.name" },
        { text: "Quantidade", value: "quantity" },
        { text: "Valor Do produto", value: "product.valueFormatted" },
        { text: "Valor Total", value: "valorFormatted" },
        { text: "Data do pedido", value: "createdAt" },
        { text: "Editar", value: "edit", sortable: false },
        { text: "Excluir", value: "delete", sortable: false },
      ],
    };
  },
  methods: {
    async getAllOrders() {
      try {
        const response = await fetch("http://localhost:5042/api/v1/order");

        const data: ApiResponse<any> = await response.json();

        if (data.success) {
          this.orders = data.result;
        } else {
          alert(`Erro: ${data.message}`);
        }
      } catch (error) {
        alert("Erro ao buscar pedidos. Por favor, tente novamente mais tarde.");
      }
    },

    formatCurrency(value: number) {
      return value.toLocaleString("pt-BR", {
        style: "currency",
        currency: "BRL",
      });
    },

    formatDate(dateString: string) {
      const date = new Date(dateString);
      const day = String(date.getDate()).padStart(2, "0");
      const month = String(date.getMonth() + 1).padStart(2, "0");
      const year = date.getFullYear();
      return `${day}-${month}-${year}`;
    },

    async deleteOrder(id: number) {
      if (confirm("Tem certeza que deseja excluir este pedido?")) {
        try {
          const response = await fetch(
            `http://localhost:5042/api/v1/order/${id}`,
            {
              method: "DELETE",
            }
          );

          if (response.ok) {
            alert("Pedido excluído com sucesso!");
            await this.getAllOrders();
          } else {
            const data = await response.json();
            alert(`Erro ao excluir pedido: ${data.message}`);
          }
        } catch (error) {
          alert(
            "Erro ao excluir pedido. Por favor, tente novamente mais tarde."
          );
        }
      }
    },
  },
  computed: {
    formattedOrders() {
      return this.orders.map((order: any) => ({
        ...order,
        product: {
          ...order.product,
          valueFormatted: this.formatCurrency(order.product.value),
        },
        valorFormatted: this.formatCurrency(order.valor),
        createdAt: this.formatDate(order.createdAt),
      }));
    },
  },
  mounted() {
    this.getAllOrders();
  },
});
</script>
